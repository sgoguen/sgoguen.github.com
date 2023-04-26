import Avatar from './avatar'
import DateFormatter from './date-formatter'
import CoverImage from './cover-image'
import Link from 'next/link'
import type Author from '../interfaces/author'

type Props = {
  title?: string
  coverImage?: string
  date?: string
  excerpt?: string
  author?: Author
  slug?: string
}

const PostPreview = (props: Props | undefined) => {
  // const { title, coverImage, date, excerpt, author, slug } = props
  return (
    <div>
      <div className="mb-5">
        <CoverImage slug={props.slug} title={props.title} src={props.coverImage} />
      </div>
      <h3 className="text-3xl mb-3 leading-snug">
        <Link
          as={`/posts/${props.slug}`}
          href="/posts/[slug]"
          className="hover:underline"
        >
          {props.title}
        </Link>
      </h3>
      <div className="text-lg mb-4">
        <DateFormatter dateString={props.date} />
      </div>
      <p className="text-lg leading-relaxed mb-4">{props.excerpt}</p>
      {/* <Avatar name={props?.author?.name} picture={props?.author?.picture} /> */}
    </div>
  )
}

export default PostPreview
