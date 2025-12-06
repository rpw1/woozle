
import { ChangeDetectionStrategy, Component, inject } from '@angular/core';
import { GameCalculationService } from '../../services/game-calculation.service';
import { ProgressSegmentComponent } from '../progress-segment/progress-segment.component';

@Component({
  selector: 'app-progress-bar',
  imports: [
    ProgressSegmentComponent
],
  templateUrl: './progress-bar.component.html',
  styleUrls: ['./progress-bar.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class ProgressBarComponent {
  private readonly gameCalculationService = inject(GameCalculationService);
  readonly guessPercentArray: number[] = this.gameCalculationService.getGamePercentageArray();
}
