import { HttpErrorResponse, HttpEvent, HttpHandler, HttpInterceptor, HttpRequest } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable, tap } from "rxjs";
import { NzMessageService } from 'ng-zorro-antd/message';
import { Router } from "@angular/router";


@Injectable()
export class SharedHttpInterceptor implements HttpInterceptor {

    constructor(private message: NzMessageService, private router: Router) {

    }

    intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {

        return next.handle(req).pipe(tap(() => { }, (error) => {
            if (error instanceof HttpErrorResponse) {
                if (error.status === 400) {
                    this.message.error("请求失败[400]")

                }
                if (error.status == 500) {
                    this.message.error("服务器繁忙！")

                }
                if (error.status == 401) {
                    this.router.navigate(['/auth/login'])

                }
                if (error.status == 403) {
                    this.message.error("当前没有访问权限")

                }
                this.message.error('网络错误！')
                return;
            }
        }));
    }

}