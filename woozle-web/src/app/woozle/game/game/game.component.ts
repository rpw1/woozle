
import {
  ChangeDetectionStrategy,
  Component,
  inject,
  OnDestroy,
  OnInit,
} from '@angular/core';
import { RouterLink } from '@angular/router';
import { GameStore } from '../game.state';
import { GuessListComponent } from '../guess-list/guess-list.component';
import { GuessComponent } from '../guess/guess.component';
import { PlayerService } from '../../spotify/player.service';
import { TracksStore } from '../../content/tracks.state';
import { ProgressBarComponent } from '../../progress-bar/progress-bar/progress-bar.component';

@Component({
  selector: 'app-game',
  imports: [
    GuessListComponent,
    GuessComponent,
    ProgressBarComponent,
    RouterLink
],
  templateUrl: './game.component.html',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class GameComponent implements OnInit, OnDestroy {
  private readonly gameStore = inject(GameStore);
  protected readonly playerService = inject(PlayerService);
  protected readonly tracksStore = inject(TracksStore);

  ngOnInit(): void {
    this.gameStore.reset();
  }

  ngOnDestroy(): void {
    this.gameStore.pauseMusic();
  }

  togglePlayerOn(): void {
    this.gameStore.playMusic();
  }

  togglePlayerOff(): void {
    this.gameStore.pauseMusic();
  }
}
