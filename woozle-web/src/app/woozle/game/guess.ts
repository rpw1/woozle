import { GuessType } from "./guess-type";

export interface Guess {
  id: string,
  type: GuessType,
  song?: string
}
