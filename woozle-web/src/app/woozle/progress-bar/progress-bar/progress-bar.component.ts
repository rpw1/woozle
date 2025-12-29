import { ChangeDetectionStrategy, Component, inject } from '@angular/core';
import { ProgressSegmentComponent } from '../progress-segment/progress-segment.component';
import { GameCalculationService } from '../../game/game-calculation.service';

@Component({
  selector: 'app-progress-bar',
  imports: [ProgressSegmentComponent],
  templateUrl: './progress-bar.component.html',
  styleUrls: ['./progress-bar.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class ProgressBarComponent {
  private readonly gameCalculationService = inject(GameCalculationService);
  readonly guessPercentArray: number[] = this.gameCalculationService.getGamePercentageArray();
}
