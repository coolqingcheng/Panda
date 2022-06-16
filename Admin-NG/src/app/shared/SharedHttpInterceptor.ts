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

       var temReq = req.clone({
            headers:req.headers.set('X-Requested-With','XMLHttpRequest')
        })

        return next.handle(temReq).pipe(tap(() => { }, (error) => {
            if (error instanceof HttpErrorResponse) {
                if (error.status === 400) {
                    this.message.error("请求失败[400]")

                } else
                    if (error.status == 500) {
                        this.message.error("服务器繁忙！")

                    } else
                        if (error.status == 401) {
                            this.router.navigate(['/auth/login'])

                        } else
                            if (error.status == 403) {
                                this.message.error("当前没有访问权限")

                            }
                            else {

                                this.message.error('网络错误！')
                            }
                return;
            }
        }));
    }

}