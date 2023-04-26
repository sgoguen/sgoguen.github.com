import { parseISO, format } from 'date-fns'

type Props = {
  dateString: string
}

const DateFormatter = ({ dateString }: Props) => {
  try {
    const date = parseISO(dateString)
    return <time dateTime={dateString}>{format(date, 'LLLL	d, yyyy')}</time>
  } catch (e) {
    return <time dateTime={dateString}>{dateString}</time>
  }
}

export default DateFormatter
