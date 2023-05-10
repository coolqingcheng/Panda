import { ElButton, ElInput, ElSwitch } from "element-plus";
import {
  defineComponent,
  render,
  PropType,
  ref,
  onMounted,
  reactive,
} from "vue";
import { BaseOption, SelectOption } from "./SimpleFormModel";

const userName = ref("111");

const status = ref(false);

const Test2 = (props: { name: string }) => {
  return (
    <>
      <h2>
        Test2:{props.name} - {userName.value} - 状态:{status.value}
      </h2>
      <ElInput v-model={userName.value}></ElInput>
      <ElSwitch v-model={status.value}></ElSwitch>
      <h1
        style={{
          background: "#ccc",
          display: "inline-block",
          userSelect: "none",
        }}
        onMousedown={(e) => {
          console.log(e.clientX, e.clientY, e.screenX, e.screenY);
          let el = e.currentTarget as HTMLElement;
          console.log(el.style.marginLeft);
          el.style.marginLeft = `${(el.style.marginLeft
            ? parseInt(el.style.marginLeft)
            : 0) + 10}px`;
        }}
      >
        点击我
      </h1>
      <br />
    </>
  );
};

const Test = defineComponent({
  props: {
    items: {
      type: Object as PropType<BaseOption[]>,
      required: true,
    },
  },
  setup(props) {
    const testHandler = () => {
      console.log("点击一下");
    };

    onMounted(() => {
      console.log("组件启动");
    });

    const point = reactive({
      x: 0,
      y: 0,
    });

    return () => (
      <>
        {props.items?.map((item) => {
          if (item.control == "input") {
            return <h1>input</h1>;
          }
          if (item.control == "select") {
            var control = item as SelectOption;
            return (
              <div>
                {control.items?.map((config) => {
                  return (
                    <p>
                      配置:{config.label} - {config.value}
                    </p>
                  );
                })}
              </div>
            );
          }
          return null;
        })}
        <Test2 name="测试姓名"></Test2>
        <ElButton onClick={testHandler}>测试-click</ElButton>
      </>
    );
  },
});

export default Test;
