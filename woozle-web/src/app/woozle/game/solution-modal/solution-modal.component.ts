import { ChangeDetectionStrategy, Component, inject, Signal } from '@angular/core';
import { NgbActiveModal, NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { SolutionStateService } from '../solution-state.service';
import { GameState } from '../game-state.model';
import { Track } from '../../content/track';

@Component({
  selector: 'app-solution-modal',
  imports: [NgbModule],
  templateUrl: './solution-modal.component.html',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class SolutionModalComponent {
  private readonly activeModal = inject(NgbActiveModal);
  private readonly solutionStateService = inject(SolutionStateService);
  readonly GameState = GameState;

  solution: Signal<Track> = this.solutionStateService.solution;

  closeModal() {
    this.activeModal.close(true);
  }
}
