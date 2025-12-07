import { Injectable, inject } from '@angular/core';
import { ValidateGameConstantsService } from './validate-game-constants.service';
import { Constants } from '../../../shared/utilities/constants';
import { GameConstants } from '../game-constants';

@Injectable({
  providedIn: 'root'
})
export class GameCalculationService {

  private readonly validateGameConstants = inject(ValidateGameConstantsService)
  private readonly guessPercentageArray: number[];

  constructor() {
    const validationErrors = this.validateGameConstants.validate();
    if (validationErrors && validationErrors.length > 0) {
      throw validationErrors;
    }

    this.guessPercentageArray = GameConstants.SECONDS_ARRAY.map((val: number) => {
      return Constants.PERCENTAGE_CONVERSION * val / GameConstants.LISTEN_SECONDS;
    });
  }

  public getGamePercentageArray(): number[] {
    return this.guessPercentageArray;
  }
}
