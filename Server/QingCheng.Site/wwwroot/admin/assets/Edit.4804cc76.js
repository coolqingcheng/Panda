import{d as C,a as i,r as B,k as y,K as A,n as g,h as a,o as x,p as S,f as l,b as I,e as u,q as p,L as w,j as r,x as N,g as R,X as U,E as q}from"./index.5c376344.js";const M={class:"title-text"},j=C({__name:"Edit",setup(P){const f=i(!1),t=i({order:0,id:0,isPublish:!1}),F=B({name:[{required:!0,message:"\u540D\u79F0\u4E0D\u80FD\u4E3A\u7A7A"}],url:[{required:!0,message:"\u57DF\u540D\u4E0D\u80FD\u4E3A\u7A7A"},{type:"url",message:"url\u5730\u5740\u9519\u8BEF"}]}),E=i(),d=y(),c=()=>{var n;(n=E.value)==null||n.validate(e=>{e&&(f.value=!0,U.edit({body:t.value}).then(()=>{q.success("\u4FDD\u5B58\u6210\u529F"),d.back()}).finally(()=>f.value=!1))})};return A(),g(()=>{const n=history.state.item;if(n){var e=n;t.value={...e}}}),(n,e)=>{const m=a("ElButton"),v=a("ElSpace"),_=a("ElInput"),s=a("ElFormItem"),D=a("ElSwitch"),b=a("ElInputNumber"),V=a("ElForm"),k=a("ElCard");return x(),S(k,null,{header:l(()=>[I("div",M,[u(v,null,{default:l(()=>[u(m,{icon:p(w),onClick:e[0]||(e[0]=()=>p(d).back()),text:""},{default:l(()=>[r("\u8FD4\u56DE")]),_:1},8,["icon"]),r(" "+N(t.value.id?"\u7F16\u8F91":"\u6DFB\u52A0")+"\u53CB\u60C5\u94FE\u63A5 ",1)]),_:1})])]),default:l(()=>[u(V,{"label-width":"60",model:t.value,onSubmit:e[7]||(e[7]=R(()=>{},["prevent"])),rules:F,ref_key:"formRef",ref:E},{default:l(()=>[u(s,{label:"\u540D\u79F0",prop:"name"},{default:l(()=>[u(_,{modelValue:t.value.name,"onUpdate:modelValue":e[1]||(e[1]=o=>t.value.name=o)},null,8,["modelValue"])]),_:1}),u(s,{label:"\u57DF\u540D",prop:"url"},{default:l(()=>[u(_,{modelValue:t.value.url,"onUpdate:modelValue":e[2]||(e[2]=o=>t.value.url=o)},null,8,["modelValue"])]),_:1}),u(s,{label:"\u516C\u5F00",prop:"url"},{default:l(()=>[u(D,{modelValue:t.value.isPublish,"onUpdate:modelValue":e[3]||(e[3]=o=>t.value.isPublish=o)},null,8,["modelValue"])]),_:1}),u(s,{label:"\u6743\u91CD",prop:"order"},{default:l(()=>[u(b,{modelValue:t.value.order,"onUpdate:modelValue":e[4]||(e[4]=o=>t.value.order=o)},null,8,["modelValue"])]),_:1}),u(s,null,{default:l(()=>[u(v,null,{default:l(()=>[u(m,{type:"primary","native-type":"submit",onClick:e[5]||(e[5]=o=>c())},{default:l(()=>[r("\u4FDD\u5B58")]),_:1}),u(m,{onClick:e[6]||(e[6]=()=>p(d).back())},{default:l(()=>[r("\u8FD4\u56DE")]),_:1})]),_:1})]),_:1})]),_:1},8,["model","rules"])]),_:1})}}});export{j as default};