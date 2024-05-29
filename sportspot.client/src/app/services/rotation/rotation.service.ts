import { Injectable, WritableSignal, signal } from '@angular/core';
import { Rotation } from '../../models/rotation';

@Injectable({
  providedIn: 'root'
})
export class RotationService {
    public rotations: WritableSignal<Rotation[] | undefined> = signal(undefined);

  constructor() { }
}
