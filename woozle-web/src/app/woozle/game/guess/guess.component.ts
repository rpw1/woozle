import { ChangeDetectionStrategy, Component, inject, signal } from '@angular/core';
import { Field, form, maxLength, required } from '@angular/forms/signals';
import { NgbTypeaheadModule } from '@ng-bootstrap/ng-bootstrap';
import { debounceTime, distinctUntilChanged, map, Observable } from 'rxjs';
import { v4 } from 'uuid';
import { TracksStore } from '../../content/tracks.state';
import { GameStore } from '../game.state';
import { Guess } from '../guess';
import { GuessType } from '../guess-type';

@Component({
  selector: 'app-guess',
  imports: [NgbTypeaheadModule, Field],
  templateUrl: './guess.component.html',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class GuessComponent {
  private readonly gameStore = inject(GameStore);
  private readonly tracksStore = inject(TracksStore);
  private readonly SKIP_GUESS_TEXT = 'SKIPPED';
  readonly GuessType = GuessType;

  search = (input: Observable<string>) =>
    input.pipe(
      debounceTime(250),
      distinctUntilChanged(),
      map(search =>
        this.tracksStore
          .tracks()
          .map(track => `${track.name} - ${track.artist}`)
          .filter(track => track.toLocaleLowerCase().includes(search.toLocaleLowerCase()))
          .slice(0, 15),
      ),
    );

  private guessModel = signal({
    currentGuess: ''
  });

  protected readonly guessForm = form(this.guessModel, (schemaPath) => {
    required(schemaPath.currentGuess, { message: 'Guess is required' });
    maxLength(schemaPath.currentGuess, 500, { message: 'Guess must be less than 500 characters.' });
  });

  submitGuess(guessType: GuessType): void {
    let guess: Guess;
    if (guessType === GuessType.SKIP) {
      guess = {
        id: v4(),
        type: GuessType.SKIP,
        song: this.SKIP_GUESS_TEXT,
      };
    } else {
      guess = {
        id: v4(),
        type: GuessType.GUESS,
        song: this.guessForm.currentGuess().value().trim(),
      };
    }
    this.gameStore.addGuess(guess);
    this.guessModel.set({
      currentGuess: ''
    });
  }
}
