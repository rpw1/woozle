import { computed, Injectable, signal } from '@angular/core';
import { Track } from '../models/track';

type Solution = {
  solution: Track,
  solutions: Track[],
  solutionIndex: number
}

const initialState: Solution = {
  solution: {
    id: '',
    name: '',
    artist: '',
    trackUri: '',
    image: {
      url: '',
    },
  },
  solutions: [],
  solutionIndex: 0,
}

@Injectable({
  providedIn: 'root'
})
export class SolutionStateService { 

  private store = signal<Solution>({...initialState});
  solutionName = computed(() => `${this.store().solution.name} - ${this.store().solution.artist}`.toLocaleLowerCase());
  solution = computed(() => this.store().solution);

  incrementSolution(): void {
    if (this.store().solutions.length === 0) {
      return;
    }

    if (this.store().solutionIndex === this.store().solutions.length) {
      this.store.update(state => ({...state, solutionIndex: 0}));
    }

    this.store.update(state => ({...state, solutionIndex: state.solutionIndex + 1, solution: state.solutions[state.solutionIndex]}));
  }

  setGameSolutions(solutions: Track[]): void {
    this.store.update(state => ({...state, solutions: solutions}));
    this.incrementSolution();
  }
}