import { inject, Injectable, signal } from '@angular/core';
import { ReplaySubject } from 'rxjs';
import { toSignal } from '@angular/core/rxjs-interop'
import { PlayerService } from '../services/player.service';
import { SolutionStateService } from './solution-state.service';
import { TaskStateType } from '../models/queue-state-type.model';

interface ProgressBarQueue {
  tasksInQueue: number,
  successiveTasksRan: number
}

const initialState: ProgressBarQueue = {
  tasksInQueue: 0,
  successiveTasksRan: 0
}

@Injectable({
  providedIn: 'root'
})
export class ProgressBarStateService { 
  private activeTaskStateSubject = new ReplaySubject<TaskStateType>(1);
  activeTaskState$ = this.activeTaskStateSubject.asObservable();
  activeTaskStateSignal = toSignal(this.activeTaskState$);

  queueState = signal<ProgressBarQueue>({ ...initialState });

  private playerService = inject(PlayerService);
  private solutionStateService = inject(SolutionStateService);

  async completeTask(): Promise<void> {
    this.queueState.update(state => ({...state, successiveTasksRan: state.successiveTasksRan + 1}));
    this.activeTaskStateSubject.next(TaskStateType.COMPLETED);
    if (this.queueState().tasksInQueue > 0) {
      this.startTask();
    } else {
      await this.resetTasks();
    }
  }

  async queueTasks(tasks: number): Promise<void> {
    this.queueState.update(state => ({...state, tasksInQueue: state.tasksInQueue + tasks}));
    if (this.queueState().tasksInQueue === 0 || this.activeTaskStateSignal() === TaskStateType.RUNNING) {
      return;
    }
    await this.startTask();
  }

  async resetTasks(): Promise<void>  {
    this.queueState.set({ ...initialState });
    this.activeTaskStateSubject.next(TaskStateType.RESET);
    if (this.playerService.isPlayingMusic()) {
      await this.playerService.togglePlayerOff();
    }
  }

  async startTask(): Promise<void> {
    if (!this.playerService.isPlayingMusic()) {
      await this.playerService.togglePlayerOn(this.solutionStateService.solution().trackUri);
    }
    this.queueState.update(state => ({...state, tasksInQueue: state.tasksInQueue - 1}));
    this.activeTaskStateSubject.next(TaskStateType.RUNNING);
  }
}