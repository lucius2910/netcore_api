import CallbackView from '@/views/auth/CallbackView.vue'
import LogoutCallbackView from '@/views/auth/LogoutCallbackView.vue'

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