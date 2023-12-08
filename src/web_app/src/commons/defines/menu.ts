export default [
  { routerName: 'Dashboard', path: '/', text: 'Dashboard', icon: 'Sunrise' },
  {
    text: 'Users',
    icon: 'Plus',
    items: [{ routerName: 'UserList', path: '/user',text: 'List' }],
  },
  {
    routerName: 'Settings',
    text: 'System settings',
    icon: 'Aim',
    items: [
      { routerName: 'RoleList', path: '/role', text: 'Role' },
      { routerName: 'FunctionList', path: '/function', text: 'Function' },
      { routerName: 'MasterCodeList', path: '/master', text: 'Master Code' },
      { routerName: 'ResourceList', path: '/resource', text: 'Resource' },
    ],
  },
  {
    text: 'Manufactures',
    icon: 'PieChart',
    routerName: 'InventoryManufacture',
    path: '/manufactures'
  },
]