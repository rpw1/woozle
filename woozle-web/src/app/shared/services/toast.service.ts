import { Injectable } from '@angular/core';
import { Subject } from 'rxjs';
import { Toast } from '../models/toast';
import { v4 } from 'uuid';

@Injectable({
  providedIn: 'root'
})
export class ToastService {
  private toasts: Toast[] = [];
  private readonly toastsSubject = new Subject<Toast[]>();
  readonly toasts$ = this.toastsSubject.asObservable();
  private readonly delay = 5000;

  showSuccess(message: string) {
    const toast: Toast = {
      id: v4(),
      message: message,
      classes: 'bg-success text-light',
      delay: this.delay
    }

    this.toasts.push(toast);
    this.toastsSubject.next(this.toasts);
  }

  showError(message: string) {
    const toast: Toast = {
      id: v4(),
      message: message,
      classes: 'bg-danger text-light',
      delay: this.delay
    }

    this.toasts.push(toast);
    this.toastsSubject.next(this.toasts);
  }

  remove(id: string) {
    this.toasts = this.toasts.filter(x => x.id !== id);
    this.toastsSubject.next(this.toasts);
  }
}
