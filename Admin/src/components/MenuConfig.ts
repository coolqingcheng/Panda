

export interface MenuItem {
    index: string
    label: string
    icon?: string
  }
  
  export interface SubMenu {
    index: string
    label: string
    icon?: string
    children: MenuItem[]
  }

  const PostMenu:SubMenu[] = [
      {
          index:'文章',
          label:'文章',
          children:[
              {
                  index:'/admin/post',
                  label:'文章管理',
              }
          ]
      }
  ]

  export {
      PostMenu
  }