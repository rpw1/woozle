
import {
  ChangeDetectionStrategy,
  Component,
  inject,
  OnDestroy,
  OnInit,
} from '@angular/core';
import { RouterLink } from '@angular/router';
import { GameStore } from '../../state/game.state';
import { GuessListComponent } from '../guess-list/guess-list.component';
import { GuessComponent } from '../guess/guess.component';
import { ProgressBarComponent } from '../progress-bar/progress-bar.component';
import { PlayerService } from '../../services/player.service';
import { TracksStore } from '../../state/tracks.state';

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
  private readonly playerService = inject(PlayerService);
  private readonly tracksStore = inject(TracksStore);
  readonly isPlayingMusic = this.playerService.isPlayingMusic;
  readonly selectedContentName = this.tracksStore.contentName;

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
