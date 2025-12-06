import { inject } from '@angular/core';
import {
  patchState,
  signalStore,
  withMethods,
  withState
} from '@ngrx/signals';
import { v4 } from 'uuid';
import { GameConstants } from '../models/game-constants';
import { Guess } from '../models/guess';
import { GuessType } from '../models/guess-type';
import { ProgressBarStateService } from './progress-bar-state.service';
import { SolutionStateService } from './solution-state.service';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { SolutionModalComponent } from '../components/solution-modal/solution-modal.component';
import { PlayerService } from '../services/player.service';
import { Game } from '../models/game.model';
import { GameState } from '../models/game-state.model';

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
  withMethods((store, 
    progressBarStateService = inject(ProgressBarStateService), 
    solutionStateService = inject(SolutionStateService),
    modalService = inject(NgbModal),
    playerService = inject(PlayerService)
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

        if (guess.song?.toLocaleLowerCase() === solutionStateService.solutionName()) {
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
        if (playerService.isPlayingMusic()) {
          await progressBarStateService.queueTasks(1);
          return;
        }

        await progressBarStateService.queueTasks(store.numberOfGuesses() + 1);
      },
      async pauseMusic() {
        await progressBarStateService.resetTasks();
      },
      async updateGameState(gameState: GameState): Promise<void> {
        patchState(store, { currentGameState: gameState, numberOfGuesses: maximumGuesses });

        if (store.currentGameState() === GameState.WON || store.currentGameState() === GameState.LOSS) {
          await this.playMusic();
          const modalRef = modalService.open(SolutionModalComponent);
          await modalRef.result;
          await this.reset();
        }
      },
      async reset(): Promise<void> {
        patchState(store, { ...initialState });
        solutionStateService.incrementSolution();
        await progressBarStateService.resetTasks();
      },
    })
  )
);
