import { Injectable } from '@angular/core';
import { GameConstants } from '../models/game-constants';

@Injectable({
  providedIn: 'root'
})
export class ValidateGameConstantsService {
  private readonly validationResults: string[] = [];
  private readonly secondsArraySum = GameConstants.SECONDS_ARRAY.reduce((prev, curr) => prev + curr, 0)
  readonly entryCountMatchesTotalGuesses = `Entry count (${GameConstants.SECONDS_ARRAY.length}) is not equal to number of total guesses (${GameConstants.TOTAL_GUESSES})`
  readonly secondsArrayAddsUpToTotalTime = `Listen seconds (${GameConstants.LISTEN_SECONDS} seconds) is not equal to the sum of seconds array (${this.secondsArraySum})`

  validate(): string[] {
    this.validateTotalGuesses();
    this.validateSecondsArray();
    return this.validationResults;
  }

  private validateTotalGuesses() {
    if (GameConstants.SECONDS_ARRAY.length != GameConstants.TOTAL_GUESSES) {
      this.validationResults.push(this.entryCountMatchesTotalGuesses)
    }
  }

  private validateSecondsArray() {
    if (GameConstants.LISTEN_SECONDS != this.secondsArraySum) {
      this.validationResults.push(this.secondsArrayAddsUpToTotalTime)
    }
  }
}
