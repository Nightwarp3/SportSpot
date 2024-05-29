import { Component, WritableSignal, input, signal } from '@angular/core';

@Component({
  selector: 'sportspot-spinner',
  templateUrl: './spinner.component.html',
  styleUrl: './spinner.component.scss'
})
export class SpinnerComponent {
    public message: WritableSignal<string> = signal('Loading...');
}
