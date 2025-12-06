import { Component, inject, OnDestroy } from '@angular/core';
import { ForbiddenErrorsService } from '../../services/forbidden-error.service';
import { AsyncPipe } from '@angular/common';

@Component({
  selector: 'app-forbidden',
  imports: [AsyncPipe],
  templateUrl: './forbidden.component.html'
})
export class ForbiddenComponent implements OnDestroy {
  private readonly forbiddenErrorsService = inject(ForbiddenErrorsService);
  readonly forbiddenErrors$ = this.forbiddenErrorsService.forbiddenErrors$

  ngOnDestroy(): void {
    this.forbiddenErrorsService.removeErrors();
  }
}
