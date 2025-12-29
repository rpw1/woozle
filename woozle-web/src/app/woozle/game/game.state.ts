import { inject } from '@angular/core';
import { patchState, signalStore, withMethods, withProps, withState } from '@ngrx/signals';
import { v4 } from 'uuid';
import { ProgressBarStateService } from '../progress-bar/progress-bar-state.service';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { SolutionModalComponent } from './solution-modal/solution-modal.component';
import { PlayerService } from '../spotify/player.service';
import { GameConstants } from './game-constants';
import { Game } from './game.model';
import { GuessType } from './guess-type';
import { GameState } from './game-state.model';
import { SolutionStateService } from './solution-state.service';
import { Guess } from './guess';

const maximumGuesses = GameConstants.SECONDS_ARRAY.length;
const initialState: Game = {
  guesses: GameConstants.SECONDS_ARRAY.map(() => ({
    id: v4(),
    type: GuessType.UNKNOWN,
  })),
  currentGameState: GameState.ACTIVE,
  numberOfGuesses: 0,
};

export const GameStore = signalStore(
  withState(initialState),
  withProps(() => ({
    progressBarStateService: inject(ProgressBarStateService),
    solutionStateService: inject(SolutionStateService),
    modalService: inject(NgbModal),
    playerService: inject(PlayerService),
  })),
  withMethods(
    (
      store,
    ) => ({
      async addGuess(guess: Guess): Promise<void> {
        patchState(store, {
          guesses: [
            ...store.guesses().slice(0, store.numberOfGuesses()),
            guess,
            ...store.guesses().slice(store.numberOfGuesses() + 1),
          ],
          numberOfGuesses: store.numberOfGuesses() + 1,
        });

        if (guess.song?.toLocaleLowerCase() === store.solutionStateService.solutionName()) {
          await this.updateGameState(GameState.WON);
          return;
        }

        if (store.numberOfGuesses() >= maximumGuesses) {
          await this.updateGameState(GameState.LOSS);
          return;
        }

        await this.playMusic();
      },
      async playMusic() {
        if (store.playerService.isPlayingMusic()) {
          await store.progressBarStateService.queueTasks(1);
          return;
        }

        await store.progressBarStateService.queueTasks(store.numberOfGuesses() + 1);
      },
      async pauseMusic() {
        await store.progressBarStateService.resetTasks();
      },
      async updateGameState(gameState: GameState): Promise<void> {
        patchState(store, {
          currentGameState: gameState,
          numberOfGuesses: maximumGuesses,
        });

        if (store.currentGameState() === GameState.WON || store.currentGameState() === GameState.LOSS) {
          await this.playMusic();
          const modalRef = store.modalService.open(SolutionModalComponent);
          await modalRef.result;
          await this.reset();
        }
      },
      async reset(): Promise<void> {
        patchState(store, { ...initialState });
        store.solutionStateService.incrementSolution();
        await store.progressBarStateService.resetTasks();
      },
    }),
  ),
);
