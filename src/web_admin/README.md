# QPON Web-admin

- [Rule code review](http://10.32.4.111:3080/root/codingstandards/-/blob/master/coding_standards_vuejs.md)
- Tham khảo cách sử dụng common component từ các màn hình có sẵn

## Built With

- [Vue 3.2.29](https://vuejs.org/) (Typescript, Composotion Api)
- [Vuetify 3.0.0-alpha.13](https://next.vuetifyjs.com/)

## Getting Started

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

Run lint

```sh
yarn lint
```

Run lint fix

```sh
yarn lint-fix
```

## Environment

### Config api url

`consfigs/_var.ts`
```
const _CONFIG = {
  BASE_URL: 'http://localhost:3000',
  API_URL: {API_URL},
}
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