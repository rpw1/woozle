import { Injectable, inject } from '@angular/core';
import { EMPTY, concatMap, filter, map, of, takeUntil, tap, timer } from 'rxjs';
import { Constants } from '../models/constants';
import { GameConstants } from '../models/game-constants';
import { ProgressBarStateService } from '../state/progress-bar-state.service';
import { TaskStateType } from '../models/queue-state-type.model';

@Injectable()
export class ProgressBarTimerService {
  private readonly progressBarStateService = inject(ProgressBarStateService)

  private timeElapsed = 0;

  private readonly timer$ = timer(0, 50).pipe(
    map(() => {
      this.timeElapsed += 1;
      const successiveTasksRan = this.progressBarStateService.queueState().successiveTasksRan;
      const decimalPercent = (this.timeElapsed * 50) / (GameConstants.SECONDS_ARRAY[successiveTasksRan] * 1000);
      return decimalPercent * Constants.PERCENTAGE_CONVERSION;
    }),
    tap(async (percent) => {
      if (percent > Constants.PERCENTAGE_CONVERSION) {
        await this.progressBarStateService.completeTask();
      }
    }),
    takeUntil(
      this.progressBarStateService.activeTaskState$.pipe(
        filter(state => state === TaskStateType.COMPLETED)
      )
    )
  );

  readonly progressBarSegmentPercentage$ = this.progressBarStateService.activeTaskState$.pipe(
    concatMap((task) => {
      switch (task) {
        case undefined: {
          return EMPTY;
        }
        case TaskStateType.RUNNING: {
          return this.timer$;
        }
        case TaskStateType.COMPLETED: {
          this.timeElapsed = 0;
          return EMPTY;
        }
        case TaskStateType.RESET: {
          return of(0);
        }
      }
    })
  );
}
