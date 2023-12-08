import { useToastStore } from '@/stores/toast.store'
import { ERROR_TEXT } from "@/commons/const";
import dateTime from '@/utils/dateTime';

const fileService = {
  resolveAndDownloadBlob(byte: any, file_name: string) {
    file_name = decodeURI(file_name)
    file_name = file_name.replace('.xlsx', `_${dateTime.formatDateTimeGrenCode((new Date).toDateString())}.xlsx`)
    const url = window.URL.createObjectURL(new Blob([byte]))
    const link = document.createElement('a')
    link.href = url
    link.setAttribute('download', file_name)
    document.body.appendChild(link)
    link.click()
    window.URL.revokeObjectURL(url)
    link.remove()
  },
  downloadAsDataURL (url: string, file_name: string) {
    file_name = decodeURI(file_name)
    file_name = file_name.replace('.pdf', `_${dateTime.formatDateTimeGrenCode((new Date).toDateString())}.pdf`)
    const link = document.createElement("a");
    link.setAttribute('download', file_name)
    link.href = url;
    link.click();
  },
  async checkFileHaveChange(file: File): Promise<any> {
    return await file.slice(0, 1) // only the first byte
      .arrayBuffer() // try to read
      .then(() => {
        // Not do anything
      })
      .catch((err: any) => {
        // error while reading
        useToastStore().push({
          type: ERROR_TEXT,
          message: "データのエクスポートに失敗しました。",
        })

        return Promise.resolve(err);
      });
  },
}

export default fileService