import { AsyncPipe } from '@angular/common';
import { ChangeDetectionStrategy, Component, inject } from '@angular/core';
import { NavigationEnd, Router, RouterLink } from '@angular/router';
import { NgbNavModule } from '@ng-bootstrap/ng-bootstrap';
import { filter, map } from 'rxjs';
import { NavItemType } from '../../models/nav-item-type';

@Component({
  selector: 'app-header',
  imports: [
    RouterLink,
    NgbNavModule,
    AsyncPipe
  ],
  templateUrl: './header.component.html',
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class HeaderComponent {
  private readonly router = inject(Router);
  readonly NavItemType = NavItemType;

  activeTab$ = this.router.events.pipe(
    filter((event): event is NavigationEnd => event instanceof NavigationEnd),
    map(navigationEnd => {
      if (navigationEnd.urlAfterRedirects.includes('home')) {
        return NavItemType.Home;
      }
      if (navigationEnd.urlAfterRedirects.includes('woozle')) {
        return NavItemType.Woozle;
      }
      return -1;
    })
  )
}
