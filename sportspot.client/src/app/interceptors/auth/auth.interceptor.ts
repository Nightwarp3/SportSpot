import { HttpEvent, HttpHandler, HttpHeaders, HttpInterceptor, HttpRequest } from '@angular/common/http';
import { Observable } from 'rxjs';
import { AuthService } from '../../services/auth/auth.service';

export class AuthInterceptor implements HttpInterceptor {
    private readonly authHeader = 'Authorization';

    constructor(private authService: AuthService) { }

    intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        if (this.authService.JwtToken?.length > 0) {
            req = req.clone({
                headers: this.setHeaders(req.headers)
            })
        }

        return next.handle(req);
    }

    private setHeaders(headers: HttpHeaders): HttpHeaders {
        let newHeaders = headers;
        if (headers.keys().indexOf(this.authHeader) !== -1) {
            newHeaders = headers.delete(this.authHeader);
        }
        return newHeaders.append(this.authHeader, `Bearer ${this.authService.JwtToken}`)
    }
};
