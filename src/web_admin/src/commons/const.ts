export enum DATA_TYPE {
  DATE = "DATE",
  DATE_TIME = "DATE_TIME",
  NUMBER = "NUMBER",
}


export enum ACC_TYPE {
  SYS_ADMIN = 1,
  ADMIN = 2,
  USER = 3,
}

export enum MESSAGE {
  DELETE_CONFIRM = "住民票情報を削除します。よろしいでしょうか。",
  UPLOAD_CONFIRM = "更新してもよろしいですか。",

  E_NO_FILE_UPLOAD = "サンプルファイルを選択してください。",
  E_API_RESPONSE = "エラーが発生しました。しばらくしてからもう一度お試しください。",
}

export const ERROR_TEXT = 'error'

export const LIMITED_TIME = 3600000 //  1 * 60 * 60 * 1000