import { GameState } from './game-state.model';
import { Guess } from './guess';

export interface Game {
  guesses: Guess[];
  numberOfGuesses: number;
  currentGameState: GameState;
}
