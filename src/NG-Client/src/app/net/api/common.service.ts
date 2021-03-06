/**
 * 后台api文档
 * No description provided (generated by Swagger Codegen https://github.com/swagger-api/swagger-codegen)
 *
 * OpenAPI spec version: v1
 * 
 *
 * NOTE: This class is auto generated by the swagger code generator program.
 * https://github.com/swagger-api/swagger-codegen.git
 * Do not edit the class manually.
 *//* tslint:disable:no-unused-variable member-ordering */

import { Inject, Injectable, Optional }                      from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams,
         HttpResponse, HttpEvent }                           from '@angular/common/http';
import { CustomHttpUrlEncodingCodec }                        from '../encoder';

import { Observable }                                        from 'rxjs';

import { StringStringValuesKeyValuePair } from '../model/stringStringValuesKeyValuePair';
import { UploadBase64Model } from '../model/uploadBase64Model';
import { UploadResult } from '../model/uploadResult';

import { BASE_PATH, COLLECTION_FORMATS }                     from '../variables';
import { Configuration }                                     from '../configuration';


@Injectable()
export class CommonService {

    protected basePath = '/';
    public defaultHeaders = new HttpHeaders();
    public configuration = new Configuration();

    constructor(protected httpClient: HttpClient, @Optional()@Inject(BASE_PATH) basePath: string, @Optional() configuration: Configuration) {
        if (basePath) {
            this.basePath = basePath;
        }
        if (configuration) {
            this.configuration = configuration;
            this.basePath = basePath || configuration.basePath || this.basePath;
        }
    }

    /**
     * @param consumes string[] mime-types
     * @return true: consumes contains 'multipart/form-data', false: otherwise
     */
    private canConsumeForm(consumes: string[]): boolean {
        const form = 'multipart/form-data';
        for (const consume of consumes) {
            if (form === consume) {
                return true;
            }
        }
        return false;
    }


    /**
     * 图片文件上传
     * 
     * @param form 
     * @param observe set whether or not to return the data Observable as the body, response or events. defaults to returning the body.
     * @param reportProgress flag to report request and response progress.
     */
    public adminUploadPostForm(form?: Array<StringStringValuesKeyValuePair>, observe?: 'body', reportProgress?: boolean): Observable<UploadResult>;
    public adminUploadPostForm(form?: Array<StringStringValuesKeyValuePair>, observe?: 'response', reportProgress?: boolean): Observable<HttpResponse<UploadResult>>;
    public adminUploadPostForm(form?: Array<StringStringValuesKeyValuePair>, observe?: 'events', reportProgress?: boolean): Observable<HttpEvent<UploadResult>>;
    public adminUploadPostForm(form?: Array<StringStringValuesKeyValuePair>, observe: any = 'body', reportProgress: boolean = false ): Observable<any> {


        let headers = this.defaultHeaders;

        // to determine the Accept header
        let httpHeaderAccepts: string[] = [
            'text/plain',
            'application/json',
            'text/json'
        ];
        const httpHeaderAcceptSelected: string | undefined = this.configuration.selectHeaderAccept(httpHeaderAccepts);
        if (httpHeaderAcceptSelected != undefined) {
            headers = headers.set('Accept', httpHeaderAcceptSelected);
        }

        // to determine the Content-Type header
        const consumes: string[] = [
            'multipart/form-data'
        ];

        const canConsumeForm = this.canConsumeForm(consumes);

        let formParams: { append(param: string, value: any): void; };
        let useForm = false;
        let convertFormParamsToString = false;
        if (useForm) {
            formParams = new FormData();
        } else {
            formParams = new HttpParams({encoder: new CustomHttpUrlEncodingCodec()});
        }

        if (form) {
            form.forEach((element) => {
                formParams = formParams.append('form', <any>element) as any || formParams;
            })
        }

        return this.httpClient.request<UploadResult>('post',`${this.basePath}/admin/upload`,
            {
                body: convertFormParamsToString ? formParams.toString() : formParams,
                withCredentials: this.configuration.withCredentials,
                headers: headers,
                observe: observe,
                reportProgress: reportProgress
            }
        );
    }

    /**
     * 
     * 
     * @param day 
     * @param file 
     * @param observe set whether or not to return the data Observable as the body, response or events. defaults to returning the body.
     * @param reportProgress flag to report request and response progress.
     */
    public imgDayFileGet(day: string, file: string, observe?: 'body', reportProgress?: boolean): Observable<any>;
    public imgDayFileGet(day: string, file: string, observe?: 'response', reportProgress?: boolean): Observable<HttpResponse<any>>;
    public imgDayFileGet(day: string, file: string, observe?: 'events', reportProgress?: boolean): Observable<HttpEvent<any>>;
    public imgDayFileGet(day: string, file: string, observe: any = 'body', reportProgress: boolean = false ): Observable<any> {

        if (day === null || day === undefined) {
            throw new Error('Required parameter day was null or undefined when calling imgDayFileGet.');
        }

        if (file === null || file === undefined) {
            throw new Error('Required parameter file was null or undefined when calling imgDayFileGet.');
        }

        let headers = this.defaultHeaders;

        // to determine the Accept header
        let httpHeaderAccepts: string[] = [
        ];
        const httpHeaderAcceptSelected: string | undefined = this.configuration.selectHeaderAccept(httpHeaderAccepts);
        if (httpHeaderAcceptSelected != undefined) {
            headers = headers.set('Accept', httpHeaderAcceptSelected);
        }

        // to determine the Content-Type header
        const consumes: string[] = [
        ];

        return this.httpClient.request<any>('get',`${this.basePath}/img/${encodeURIComponent(String(day))}/${encodeURIComponent(String(file))}`,
            {
                withCredentials: this.configuration.withCredentials,
                headers: headers,
                observe: observe,
                reportProgress: reportProgress
            }
        );
    }

    /**
     * base64图片上传
     * 
     * @param body 
     * @param observe set whether or not to return the data Observable as the body, response or events. defaults to returning the body.
     * @param reportProgress flag to report request and response progress.
     */
    public uploadbase64Post(body?: UploadBase64Model, observe?: 'body', reportProgress?: boolean): Observable<UploadResult>;
    public uploadbase64Post(body?: UploadBase64Model, observe?: 'response', reportProgress?: boolean): Observable<HttpResponse<UploadResult>>;
    public uploadbase64Post(body?: UploadBase64Model, observe?: 'events', reportProgress?: boolean): Observable<HttpEvent<UploadResult>>;
    public uploadbase64Post(body?: UploadBase64Model, observe: any = 'body', reportProgress: boolean = false ): Observable<any> {


        let headers = this.defaultHeaders;

        // to determine the Accept header
        let httpHeaderAccepts: string[] = [
            'text/plain',
            'application/json',
            'text/json'
        ];
        const httpHeaderAcceptSelected: string | undefined = this.configuration.selectHeaderAccept(httpHeaderAccepts);
        if (httpHeaderAcceptSelected != undefined) {
            headers = headers.set('Accept', httpHeaderAcceptSelected);
        }

        // to determine the Content-Type header
        const consumes: string[] = [
            'application/json',
            'text/json',
            'application/_*+json'
        ];
        const httpContentTypeSelected: string | undefined = this.configuration.selectHeaderContentType(consumes);
        if (httpContentTypeSelected != undefined) {
            headers = headers.set('Content-Type', httpContentTypeSelected);
        }

        return this.httpClient.request<UploadResult>('post',`${this.basePath}/uploadbase64`,
            {
                body: body,
                withCredentials: this.configuration.withCredentials,
                headers: headers,
                observe: observe,
                reportProgress: reportProgress
            }
        );
    }

}
