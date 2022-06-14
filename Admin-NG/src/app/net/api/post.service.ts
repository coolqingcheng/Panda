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

import { AdminPostItemResponsePageDto } from '../model/adminPostItemResponsePageDto';
import { PostAddOrUpdate } from '../model/postAddOrUpdate';
import { PostDetailItem } from '../model/postDetailItem';

import { BASE_PATH, COLLECTION_FORMATS }                     from '../variables';
import { Configuration }                                     from '../configuration';


@Injectable()
export class PostService {

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
     * 
     * 
     * @param body 
     * @param observe set whether or not to return the data Observable as the body, response or events. defaults to returning the body.
     * @param reportProgress flag to report request and response progress.
     */
    public adminPostAddOrUpdatePost(body?: PostAddOrUpdate, observe?: 'body', reportProgress?: boolean): Observable<any>;
    public adminPostAddOrUpdatePost(body?: PostAddOrUpdate, observe?: 'response', reportProgress?: boolean): Observable<HttpResponse<any>>;
    public adminPostAddOrUpdatePost(body?: PostAddOrUpdate, observe?: 'events', reportProgress?: boolean): Observable<HttpEvent<any>>;
    public adminPostAddOrUpdatePost(body?: PostAddOrUpdate, observe: any = 'body', reportProgress: boolean = false ): Observable<any> {


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
            'application/json',
            'text/json',
            'application/_*+json'
        ];
        const httpContentTypeSelected: string | undefined = this.configuration.selectHeaderContentType(consumes);
        if (httpContentTypeSelected != undefined) {
            headers = headers.set('Content-Type', httpContentTypeSelected);
        }

        return this.httpClient.request<any>('post',`${this.basePath}/admin/Post/AddOrUpdate`,
            {
                body: body,
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
     * @param id 
     * @param observe set whether or not to return the data Observable as the body, response or events. defaults to returning the body.
     * @param reportProgress flag to report request and response progress.
     */
    public adminPostDeleteDelete(id?: number, observe?: 'body', reportProgress?: boolean): Observable<any>;
    public adminPostDeleteDelete(id?: number, observe?: 'response', reportProgress?: boolean): Observable<HttpResponse<any>>;
    public adminPostDeleteDelete(id?: number, observe?: 'events', reportProgress?: boolean): Observable<HttpEvent<any>>;
    public adminPostDeleteDelete(id?: number, observe: any = 'body', reportProgress: boolean = false ): Observable<any> {


        let queryParameters = new HttpParams({encoder: new CustomHttpUrlEncodingCodec()});
        if (id !== undefined && id !== null) {
            queryParameters = queryParameters.set('id', <any>id);
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

        return this.httpClient.request<any>('delete',`${this.basePath}/admin/Post/Delete`,
            {
                params: queryParameters,
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
     * @param id 
     * @param observe set whether or not to return the data Observable as the body, response or events. defaults to returning the body.
     * @param reportProgress flag to report request and response progress.
     */
    public adminPostGetGet(id?: number, observe?: 'body', reportProgress?: boolean): Observable<PostDetailItem>;
    public adminPostGetGet(id?: number, observe?: 'response', reportProgress?: boolean): Observable<HttpResponse<PostDetailItem>>;
    public adminPostGetGet(id?: number, observe?: 'events', reportProgress?: boolean): Observable<HttpEvent<PostDetailItem>>;
    public adminPostGetGet(id?: number, observe: any = 'body', reportProgress: boolean = false ): Observable<any> {


        let queryParameters = new HttpParams({encoder: new CustomHttpUrlEncodingCodec()});
        if (id !== undefined && id !== null) {
            queryParameters = queryParameters.set('id', <any>id);
        }

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
        ];

        return this.httpClient.request<PostDetailItem>('get',`${this.basePath}/admin/Post/Get`,
            {
                params: queryParameters,
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
     * @param index 
     * @param size 
     * @param keyWord 
     * @param observe set whether or not to return the data Observable as the body, response or events. defaults to returning the body.
     * @param reportProgress flag to report request and response progress.
     */
    public adminPostGetListGet(index: number, size: number, keyWord?: string, observe?: 'body', reportProgress?: boolean): Observable<AdminPostItemResponsePageDto>;
    public adminPostGetListGet(index: number, size: number, keyWord?: string, observe?: 'response', reportProgress?: boolean): Observable<HttpResponse<AdminPostItemResponsePageDto>>;
    public adminPostGetListGet(index: number, size: number, keyWord?: string, observe?: 'events', reportProgress?: boolean): Observable<HttpEvent<AdminPostItemResponsePageDto>>;
    public adminPostGetListGet(index: number, size: number, keyWord?: string, observe: any = 'body', reportProgress: boolean = false ): Observable<any> {

        if (index === null || index === undefined) {
            throw new Error('Required parameter index was null or undefined when calling adminPostGetListGet.');
        }

        if (size === null || size === undefined) {
            throw new Error('Required parameter size was null or undefined when calling adminPostGetListGet.');
        }


        let queryParameters = new HttpParams({encoder: new CustomHttpUrlEncodingCodec()});
        if (keyWord !== undefined && keyWord !== null) {
            queryParameters = queryParameters.set('KeyWord', <any>keyWord);
        }
        if (index !== undefined && index !== null) {
            queryParameters = queryParameters.set('Index', <any>index);
        }
        if (size !== undefined && size !== null) {
            queryParameters = queryParameters.set('Size', <any>size);
        }

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
        ];

        return this.httpClient.request<AdminPostItemResponsePageDto>('get',`${this.basePath}/admin/Post/GetList`,
            {
                params: queryParameters,
                withCredentials: this.configuration.withCredentials,
                headers: headers,
                observe: observe,
                reportProgress: reportProgress
            }
        );
    }

}