import { CommonModule } from '@angular/common';
import { ChangeDetectionStrategy, Component, inject } from '@angular/core';
import {
  NonNullableFormBuilder,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { NgbTypeaheadModule } from '@ng-bootstrap/ng-bootstrap';
import {
  debounceTime,
  defer,
  distinctUntilChanged,
  EMPTY,
  map,
  Observable,
  startWith,
} from 'rxjs';
import { v4 } from 'uuid';
import { Guess } from '../../models/guess';
import { GuessType } from '../../models/guess-type';
import { GameStore } from '../../state/game.state';
import { TracksStore } from '../../state/tracks.state';

@Component({
  selector: 'app-guess',
  imports: [ReactiveFormsModule, CommonModule, NgbTypeaheadModule],
  templateUrl: './guess.component.html',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class GuessComponent {
  private readonly gameStore = inject(GameStore);
  private readonly tracksStore = inject(TracksStore);
  private readonly formBuilder = inject(NonNullableFormBuilder);
  private readonly SKIP_GUESS_TEXT = 'SKIPPED';
  readonly GuessType = GuessType;

  search = (input: Observable<string>) =>
    input.pipe(
      debounceTime(250),
      distinctUntilChanged(),
      map((search) => {
        return this.tracksStore
          .tracks()
          .map((track) => `${track.name} - ${track.artist}`)
          .filter((track) =>
            track.toLocaleLowerCase().includes(search.toLocaleLowerCase())
          )
          .slice(0, 15);
      })
    );

  readonly guessForm = this.formBuilder.group({
    currentGuess: ['', [Validators.required, Validators.maxLength(500)]],
  });
  get currentGuess() {
    return this.guessForm.get('currentGuess');
  }
  isCurrentGuessInvalid$ = defer(
    () =>
      this.currentGuess?.statusChanges.pipe(
        startWith(this.currentGuess.status),
        map((x) => x === 'INVALID')
      ) ?? EMPTY
  );

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
        song: this.currentGuess?.value.trim(),
      };
    }
    this.gameStore.addGuess(guess);
    this.guessForm.reset();
  }
}
