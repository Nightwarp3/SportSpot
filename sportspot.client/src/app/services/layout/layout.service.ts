import { Overlay, OverlayRef } from '@angular/cdk/overlay';
import { Injectable } from '@angular/core';
import { SpinnerComponent } from '../../components/spinner/spinner.component';
import { ComponentPortal } from '@angular/cdk/portal';

@Injectable({
    providedIn: 'root'
})
export class LayoutService {
    private overlayRef!: OverlayRef;

    constructor(private overlay: Overlay) { }

    public showSpinner(message: string = ''): void {
        this.overlayRef = this.overlay.create({
            hasBackdrop: true,
            positionStrategy: this.overlay.position().global().centerHorizontally().centerVertically()
        });

        const spinnerPortal = new ComponentPortal(SpinnerComponent, );
        const componentRef = this.overlayRef.attach(spinnerPortal);
        componentRef.instance.message.set(message);
    }

    public hideSpinner(): void {
        if (this.overlayRef?.hasAttached()) {
            this.overlayRef.detach();
            this.overlayRef.dispose();
        }
    }
}
