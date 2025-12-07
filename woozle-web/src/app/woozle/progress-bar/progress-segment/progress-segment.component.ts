import { CommonModule } from '@angular/common';
import {
  ChangeDetectionStrategy,
  Component,
  inject,
  input
} from '@angular/core';
import { map } from 'rxjs';
import { GameStore } from '../../game/game.state';
import { ProgressBarStateService } from '../progress-bar-state.service';
import { ProgressBarTimerService } from '../progress-bar-timer.service';
import { TaskStateType } from '../queue-state-type.model';

@Component({
  selector: 'app-progress-segment',
  imports: [CommonModule],
  providers: [ProgressBarTimerService],
  templateUrl: './progress-segment.component.html',
  styleUrl: './progress-segment.component.scss',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class ProgressSegmentComponent {
  segmentIndex = input.required<number>();

  private readonly gameStore = inject(GameStore);
  private readonly progressBarStateService = inject(ProgressBarStateService);
  private readonly progressBarTimerService = inject(ProgressBarTimerService);
  readonly numberOfGuesses = this.gameStore.numberOfGuesses;

  readonly progressWidth$ =
    this.progressBarTimerService.progressBarSegmentPercentage$.pipe(
      map((percent) => {
        if (this.progressBarStateService.activeTaskStateSignal() === TaskStateType.RESET) {
          return 0;
        }

        if (this.segmentIndex() === this.progressBarStateService.queueState().successiveTasksRan) {
          return percent;
        } else if (this.segmentIndex() < this.progressBarStateService.queueState().successiveTasksRan) {
          return 100;
        } else {
          return 0;
        }
      })
    );
}
