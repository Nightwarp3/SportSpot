import { Injectable, WritableSignal, signal } from '@angular/core';
import { Substitution } from '../../models/substitution';

@Injectable({
  providedIn: 'root'
})
export class SubstitutionService {
    public substitutions: WritableSignal<Substitution[] | undefined> = signal(undefined);

  constructor() { }
}
