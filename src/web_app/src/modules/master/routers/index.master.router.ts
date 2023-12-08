import user from '@master/routers/user.router'
import functions from '@master/routers/functions.router'
import role from '@master/routers/role.router'
import master from '@master/routers/master.router'

export default [...user, ...functions, ...role, ...master]