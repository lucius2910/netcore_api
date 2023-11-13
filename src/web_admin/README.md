# Web-admin

- [Rule code review](http://10.32.4.160:3080/root/code-base/-/blob/master/coding_standards_vuejs.md)
- Tham khảo cách sử dụng common component từ các màn hình có sẵn

## Built With

- [Vue 3.2.29](https://vuejs.org/) (Typescript, Composotion Api)
- [Element-plus 2.2.8](https://element-plus.org/en-US)

## Getting Started

### VsCode extention

- ESLint
- Prettier - Code formatter
- TypeScript Vue Plugin (Volar)
- Vue Language Features (Volar)

### Prerequisites

- node 16.13.0
- npm 8.1.0

## Installation

```bash
# install dependencies
$ npm install

# serve with hot reload at localhost:3000
$ npm run dev

# generate static project
$ npm run build
```

## Usage

http://localhost:3000/

### Development

Run check code format & convention

```sh
yarn lint-fix
```

## Environment

### Config api url

`.env`

```
VITE_API_URL=https://localhost:7075/api
```

## Folder Structure Conventions

### Directory layout

    .src
    ├── assets                  # Compiled files
    ├── commons                 # Common data
    │   ├── tables              # Define page table structure
    │   │   ├── user.ts         # Table list user
    │   │   └── permission.ts   # Table permission
    │   ├── constant.ts         # Define constant: api path, ...
    │   └── menu.ts             # Define navigation structure
    ├── components              # Source files
    │   ├── commons             # Form common component
    │   │   ├── _vc_register.ts # Register common
    │   │   └── vc-button.vue   # Common button
    │   └── layouts             # Layout component
    ├── configs                 # Config global variable
    ├── interface               # Declare interface api
    ├── layouts                 # All layoutt template
    ├── plugin                  # Import vue plugin: vuetify
    ├── router                  # Declare page router
    ├── services                # Service call api
    │   ├── _constant.ts        # Declare api path
    │   ├── user.ts
    │   └── auth.ts
    ├── utils                   # Common helper functions: date time, validation, ...
    ├── stores                  # State management
    │   └── auth.ts
    └── README.md               # Source document

# Code sample

### Layouts

- Don't edit `TheAppLayout.vue`
- Thêm mới layout & đặt tên theo rule
  s
  `The + [Layout_Name] + Layout.vue`

      VD: `TheClientLayout`, `TheDefaultLayout`

- Khai báo layout màn hình bằng cách thêm thuộc tính trong router
  VD:
  ```js
		{
			path: '/admin/group-user',
			name: 'GroupUser',
			component: () => import('@/views/admin/GroupUser.vue'),
			meta: {
				layout: "Client"
			}
		},
		{
			path: '/change-password',
			name: 'ChangePassWord',
			component: () => import('@/views/account/ChangePassword.vue'),
			meta: {
				layout: "Default"
			}
		},
```
