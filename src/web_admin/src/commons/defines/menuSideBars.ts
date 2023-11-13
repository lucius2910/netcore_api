export default [
  {
    title: 'アカウント',
    icon: 'UserFilled',
    permission: ['2', '3'],
    items: [
      {
        name: 'アカウント情報',
        pathName: 'AccountProfile', permission: ['2', '3'],
      },
      {
        name: 'パスワード変更',
        pathName: 'ChangePassWord', permission: ['2', '3'],
      },
      {
        name: 'メールアドレス変更',
        pathName: 'ChangeEmail', permission: ['2', '3'],
      },
    ],
  },
]
