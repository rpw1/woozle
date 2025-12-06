import {
  ChangeDetectionStrategy,
  Component,
  input,
  output,
} from '@angular/core';
import { Content } from '../../models/content';

@Component({
  selector: 'app-content',
  imports: [],
  templateUrl: './content.component.html',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class ContentComponent {
  content = input.required<Content>();
  selectedContent = output<Content>();
}
