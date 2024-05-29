import { Injectable, WritableSignal, signal } from '@angular/core';
import { Position } from '../../models/position';

@Injectable({
  providedIn: 'root'
})
export class PositionService {
    public positions: WritableSignal<Position[] | undefined> = signal(undefined);

  constructor() { }
}
