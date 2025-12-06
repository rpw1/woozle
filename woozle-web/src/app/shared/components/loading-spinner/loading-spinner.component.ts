import { AsyncPipe } from '@angular/common';
import { Component, inject, OnDestroy, OnInit } from '@angular/core';
import { NavigationEnd, NavigationStart, Router } from '@angular/router';
import { Subject, takeUntil, tap } from 'rxjs';
import { LoadingSpinnerService } from '../../services/loading-spinner.service';

@Component({
  selector: 'app-loading-spinner',
  imports: [AsyncPipe],
  templateUrl: './loading-spinner.component.html',
  styleUrl: './loading-spinner.component.scss'
})
export class LoadingSpinnerComponent implements OnInit, OnDestroy {
  private readonly loadingSpinnerService = inject(LoadingSpinnerService);
  private readonly router = inject(Router);

  readonly loading$ = this.loadingSpinnerService.loading$;
  private readonly destroy$ = new Subject<void>();

  ngOnInit(): void {
    this.router.events.pipe(
      tap((event) => {
        if (event instanceof NavigationStart) {
          this.loadingSpinnerService.startLoading();
        } else if (event instanceof NavigationEnd) {
          this.loadingSpinnerService.stopLoading();
        }
      }),
      takeUntil(this.destroy$)
    ).subscribe();
  }

  ngOnDestroy(): void {
    this.destroy$.next();
  }
}
