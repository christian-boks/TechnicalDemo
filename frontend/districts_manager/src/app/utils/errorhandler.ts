import { Injectable, Provider } from "@angular/core";
import {
    HTTP_INTERCEPTORS,
    HttpEvent,
    HttpHandler,
    HttpInterceptor,
    HttpRequest
} from "@angular/common/http";
import { Observable, throwError } from "rxjs";
import { catchError } from "rxjs/operators";
import { Router } from "@angular/router";

@Injectable()
export class HttpInterceptorService implements HttpInterceptor {
    constructor(public router: Router) { }

    intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        return next.handle(req).pipe(
            catchError(error => {
                console.error("Caught error:", error.error);
                return throwError(() => error.error);
            })
        );
    }
}

export const errorInterceptorProvider: Provider = { provide: HTTP_INTERCEPTORS, useClass: HttpInterceptorService, multi: true };