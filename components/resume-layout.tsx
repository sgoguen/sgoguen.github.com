import Alert from './alert'
import Footer from './footer'
import Meta from './meta'

type Props = {
  preview?: boolean
  children: React.ReactNode
}

const ResumeLayout = ({ preview, children }: Props) => {
  return (
    <>
      <Meta />
      <CSS />
      <div className="min-h-screen">
        {/* <Alert preview={preview} /> */}
        <main>{children}</main>
      </div>
    </>
  )
}

export default ResumeLayout


function CSS() {
  return <style jsx global>{`
  body {
    margin: 10px;
}

p,
td,
span,
li,
a {
    font-family: verdana, arial, helvetica, sans-serif;
}

h1 {
    font: 90% verdana, arial, helvetica, sans-serif;
    font-weight: bold;
    margin: 0 0 5px 0;
}

h2 {
    border-bottom: 1px solid #666;
    clear: both;
    font: 80% verdana, arial, helvetica, sans-serif;
    font-weight: bold;
    margin: 15px 0 10px 0;
    padding: 0 0 3px 0;
    width: 700px;
}

h3 {
    float: left;
    font: 80% verdana, arial, helvetica, sans-serif;
    font-weight: bold;
    margin: 0 0 5px 0;
    padding: 0;
    width: 446px;
}

ul {
  list-style-type: disc;
}

li {
    font-size: 70%;
    margin: 0 0 3px 20px;
    padding: 0;
}

p,
table {
    font-size: 70%;
    margin: 0 0 10px 0;
    padding: 0;
    width: 575px;
}

body ul {
    margin: 0 0 0 125px;
    padding: 0;
    width: 575px;
}

body div ul {
    margin: 3px 0 0 0;
    padding: 0;
    width: 575px;
}

#skills {
    margin-bottom: 100px;
}

ul#skills li {
    float: left;
    width: 20em;
}

#bio {
    display: flex;
    flex-direction: row;
    width: 700px;
}

#bio_left {
    font-size: 70%;
}

#bio_right {
    font-size: 70%;
    text-align: right;
}

.group {
    clear: both;
    margin: 0 0 5px 0;
    padding: 0;
}

.data {
    padding-left: 125px;
}

.date {
    clear: left;
    float: left;
    padding-top: 2px;
    width: 125px;
}

.job {
    clear: both;
    width: 700px;
}

.job_data {
    float: left;
    width: 575px;
    margin-bottom: 10px;
}

.location {
    clear: right;
    float: left;
    text-align: right;
    width: 125px;
}

.position {
    font-style: italic;
    margin: 0 0 5px 0;
}

#references {
    margin-top: 20px;
}
  `}</style>
}

function PrintCSS() {
  return <style media="print" jsx global>{`
  a:link {
      text-decoration: none;
      color: black;
  }
  
  a:visited {
      color: black;
  }
  `}
  </style>
}