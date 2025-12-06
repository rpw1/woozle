import { AsyncPipe } from '@angular/common';
import { Component, inject } from '@angular/core';
import { NgbToast } from '@ng-bootstrap/ng-bootstrap';
import { ToastService } from '../../services/toast.service';

@Component({
  selector: 'app-toast',
  standalone: true,
  imports: [NgbToast, AsyncPipe],
  templateUrl: './toast.component.html'
})
export class ToastComponent {
  private readonly toastService = inject(ToastService);
  readonly toasts$ = this.toastService.toasts$;

  removeToast(id: string) {
    this.toastService.remove(id);
  }
}
