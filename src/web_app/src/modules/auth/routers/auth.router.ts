import CallbackView from '@auth/views/auth/CallbackView.vue'
import LogoutCallbackView from '@auth/views/auth/LogoutCallbackView.vue'

export default  [
	{
    path: '/callback',
    name: 'Callback',
    component: CallbackView,
    meta: {
      layout: "Client"
    }
  },
  {
    path: '/logout-callback',
    name: 'LogoutCallback',
    component: LogoutCallbackView,
    meta: {
      layout: "Client"
    }
  },
]