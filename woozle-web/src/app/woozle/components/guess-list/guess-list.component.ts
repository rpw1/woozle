
import { ChangeDetectionStrategy, Component, inject } from '@angular/core';
import { GuessType } from '../../models/guess-type';
import { GameStore } from '../../state/game.state';

@Component({
  selector: 'app-guess-list',
  imports: [],
  templateUrl: './guess-list.component.html',
  styleUrls: ['./guess-list.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class GuessListComponent {

  guessType = GuessType;

  private readonly gameStore = inject(GameStore);
  readonly guesses = this.gameStore.guesses;
}
