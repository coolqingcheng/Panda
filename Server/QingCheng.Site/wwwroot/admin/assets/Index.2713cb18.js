import{d as w,k as x,a as h,r as _,l as k,M as B,n as N,h as i,o as V,p as y,f as t,e as a,q as e,N as r,s as T,t as R,O as S,v as C,Q as u,j as E,R as M,b as P}from"./index.5c376344.js";import{t as $,p as q}from"./ElConfig.eb5931ef.js";const D=P("div",{class:"text-title"}," \u5206\u7C7B ",-1),z=w({__name:"Index",setup(I){const f=x(),m=l=>{l!=null?f.push({name:"\u7F16\u8F91\u5206\u7C7B",query:{id:l.id,cateName:l.cateName}}):f.push({name:"\u7F16\u8F91\u5206\u7C7B"})};var p=h(!1);const o=_({index:1,pageSize:10,total:0});k(()=>o.index,()=>{c()});var d=_([]);const c=()=>{p.value=!0,B.getList({...o}).then(l=>{d=[],l.data&&(d=l.data),console.log(d),o.total=l.total}).finally(()=>p.value=!1)};return N(()=>{c()}),(l,n)=>{const F=i("ElSpace"),g=i("ElCol"),v=i("ElRow"),b=i("ElPagination");return V(),y(e(M),null,{header:t(()=>[D]),default:t(()=>[a(v,null,{default:t(()=>[a(g,null,{default:t(()=>[a(F,null,{default:t(()=>[a(e(r),{icon:e(T),onClick:n[0]||(n[0]=s=>m(null))},null,8,["icon"]),a(e(r),{icon:e(R),onClick:n[1]||(n[1]=s=>c())},null,8,["icon"])]),_:1})]),_:1})]),_:1}),a(e(S),C({data:e(d),loading:e(p)},e($)),{default:t(()=>[a(e(u),{label:"Id",width:"80",prop:"id"}),a(e(u),{label:"\u540D\u79F0",prop:"cateName"}),a(e(u),{label:"\u5173\u8054\u6570\u91CF",width:"120",prop:"postCount"}),a(e(u),{label:"\u521B\u5EFA\u65F6\u95F4",width:"180",prop:"createTime"}),a(e(u),{label:"\u6700\u540E\u4FEE\u6539\u65F6\u95F4",width:"180",prop:"lastUpdateTime"}),a(e(u),{label:"\u64CD\u4F5C",width:"120"},{default:t(s=>[a(e(r),{type:"primary",link:"",onClick:L=>m(s.row)},{default:t(()=>[E("\u7F16\u8F91")]),_:2},1032,["onClick"]),a(e(r),{type:"danger",link:""},{default:t(()=>[E("\u5220\u9664")]),_:1})]),_:1})]),_:1},16,["data","loading"]),a(b,C(e(q),{total:o.total,modelValue:o.index,"onUpdate:modelValue":n[2]||(n[2]=s=>o.index=s)}),null,16,["total","modelValue"])]),_:1})}}});export{z as default};