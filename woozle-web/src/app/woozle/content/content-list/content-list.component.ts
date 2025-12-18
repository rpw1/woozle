import { ChangeDetectionStrategy, Component, inject } from '@angular/core';
import { NonNullableFormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { NgbNavModule } from '@ng-bootstrap/ng-bootstrap';
import { ContentFilters, ContentFilterType, ContentsStore } from '../contents.state';
import { TracksStore } from '../tracks.state';
import { ContentComponent } from '../../content/content/content.component';
import { SolutionStateService } from '../../game/solution-state.service';
import { Content } from '../content';

@Component({
  selector: 'app-content-list',
  imports: [ContentComponent, NgbNavModule, ReactiveFormsModule],
  templateUrl: './content-list.component.html',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class ContentListComponent {
  private readonly formBuilder = inject(NonNullableFormBuilder);
  protected readonly contentsStore = inject(ContentsStore);
  private readonly tracksStore = inject(TracksStore);
  private readonly router = inject(Router);
  private readonly solutionStateService = inject(SolutionStateService);

  readonly contentSearchForm = this.formBuilder.group({
    contentSearchInput: ['', [Validators.maxLength(500)]],
  });
  get contentSearchInput() {
    return this.contentSearchForm.get('contentSearchInput');
  }

  search() {
    const filters: ContentFilters = {
      filterType: ContentFilterType.Recent,
      name: this.contentSearchInput?.value,
    };
    this.contentsStore.updateFilters(filters);
  }

  setContent(content: Content) {
    this.tracksStore.loadTracks(content);
    this.solutionStateService.setGameSolutions(this.tracksStore.randomTracks());
    void this.router.navigate(['/woozle/play']);
  }
}
