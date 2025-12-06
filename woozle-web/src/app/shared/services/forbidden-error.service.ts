import { Injectable } from '@angular/core';
import { ReplaySubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ForbiddenErrorsService {
  private forbiddenErrors: string[] = [];
  private readonly forbiddenErrorsSubject = new ReplaySubject<string[]>(1);
  readonly forbiddenErrors$ = this.forbiddenErrorsSubject.asObservable();

  addErrors(...errors: string[]) {
    this.forbiddenErrors.push(...errors);
    this.forbiddenErrorsSubject.next(this.forbiddenErrors);
  }

  removeErrors(){
    this.forbiddenErrors = [];
    this.forbiddenErrorsSubject.next(this.forbiddenErrors);
  }
}
