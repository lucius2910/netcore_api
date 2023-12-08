export default {
  getCurrentDate() {
    return new Date().toISOString().slice(0, 10)
  },
  timeStampToString(timeStamp: any) {
    return timeStamp
      ? this.formatDateTime(new Date(timeStamp * 1000).toDateString())
      : ''
  },
  //convert date data form string
  formatDate(input: string) {
    return input ? new Date(input).toISOString().slice(0, 10) : null
  },
  formatDateTime(input: string) {
    return input
      ? new Date(input).toISOString().replace(/T/, ' ').replace(/\..+/, '')
      : null
  },
  formatDatenew(input: string) {
    const date = new Date(input)
    if (!isNaN(date.getTime())) {
      const m = Number(date.getMonth() + 1)
      const d = Number(date.getDate())
      if (m < 10) {
        if (d < 10) {
          return date.getFullYear() + '/' + '0' + m + '/' + '0' + d
        } else {
          return date.getFullYear() + '/' + '0' + m + '/' + d
        }
      }
      {
        if (d < 10) {
          return date.getFullYear() + '/' + m + '/' + '0' + d
        } else {
          return date.getFullYear() + '/' + m + '/' + d
        }
      }
    }
  },
  formatDateTimeNew(input: string) {
    if (!input) {
      return null
    }

    const date = new Date(input)
    const year = date.getFullYear()
    const month = String(date.getMonth() + 1).padStart(2, '0')
    const day = String(date.getDate()).padStart(2, '0')
    const hours = String(date.getHours()).padStart(2, '0')
    const minutes = String(date.getMinutes()).padStart(2, '0')
    const seconds = String(date.getSeconds()).padStart(2, '0')

    return `${year}-${month}-${day} ${hours}:${minutes}:${seconds}`
  },
  formatDateTimeGrenCode(input: string) {
    const date = new Date(input)
    const year = date.getFullYear()
    const month = String(date.getMonth() + 1).padStart(2, '0')
    const day = String(date.getDate()).padStart(2, '0')

    return `${year}${month}${day}`
  },
  formatDateNew(input: string) {
    if (!input) {
      return null
    }

    const date = new Date(input)
    const year = date.getFullYear()
    const month = String(date.getMonth() + 1).padStart(2, '0')
    const day = String(date.getDate()).padStart(2, '0')
    return `${year}-${month}-${day}`
  },
  encodeDateToTimestamp(date: any) {
    if (!(date instanceof Date)) {
      throw new Error('Invalid input. Please provide a valid Date object.')
    }

    return Math.floor(date.getTime() / 1000)
  },
  decodeTimestampToDate(timestamp: any) {
    if (typeof timestamp !== 'number' || isNaN(timestamp)) {
      throw new Error(
        'Invalid input. Please provide a valid timestamp as a number.'
      )
    }
    return new Date(timestamp * 1000)
  },
}
