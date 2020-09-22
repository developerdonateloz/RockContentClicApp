import React from 'react';
import axios from 'axios'

async function getArticulos(){
  try{
    const response=await axios({
      // url:`{baseURL}/Articles`,
      url:'https://localhost:44353/api/Articles',
      method:'GET'
    })
    //console.log(response.data)
    
    return response.data
  }
  catch(e){
    console.log(e)
  }
}
async function postLike(article){
  try{
    const response=await axios({
      url:'https://localhost:44353/api/Likes',
      method:'POST',
      data: article,
    })

    return response.data
  }
  catch(ex){
    console.log(ex)
  }
}

function App() {
  const [listadearticulos,setListaDeArticulos]=React.useState([])
  // const [idarticle,setIdArticle]=React.useState('')
  const [msjeerror,setMsjeError]=React.useState('')
  const [msjeerrorL,setMsjeErrorL]=React.useState('')

  const Listar=async e=>{
    const r=await getArticulos()
    if(r===undefined || r===null)
    {
      setMsjeErrorL('Please wait for 5 seconds to clic again to check list of articles')  
      setMsjeError('')
    }
    else
    {
      setListaDeArticulos(r)
      setMsjeErrorL('')
    }
    
    return listadearticulos
  }
  const GuardarLike=async (item)=>{
    // setIdArticle(item.id)
    
    const article={
      ArticleId:item.id,
    }
    console.log(article)
    const t=await postLike(article)
    console.log(t)

    if(t===undefined)
    {
      setMsjeError('Plase wait for 5 seconds to clic Like again')  
      setMsjeErrorL('')
    }
    else
    {
      setMsjeError('')  
      setListaDeArticulos(t)  
    }
  }

  return (
    <div className="container">
      <h1 className="text-center">Technical Test - Dónaghy Amachi</h1>
      <hr/>
      <div className="row">
        <div className="col-8">
          <div className="row">
            <div className="col-8">
            <h4 className="text-center">Articles List</h4>
            </div>
            <div className="col-4">
            <button className="btn btn-warning" onClick={Listar}>View Articles</button>
            </div>
          </div>
          <ul className="list-group">  
          {
            listadearticulos.map((item,id)=>(
              <li key={item.id} className="list-group-item">
              <span className="lead">{item.content}</span>
            <button className="btn btn-primary btn-sm float-right" onClick={()=>GuardarLike(item)}>{item.numberLikes} likes</button>
            </li>
            ))
          }
          </ul>
          {msjeerrorL!==''?<div className="alert alert-danger">{msjeerrorL}</div>:<div></div>}
          {
            msjeerror!==''?<div className="alert alert-danger">{msjeerror}</div>:<div></div>
          }
        </div>
        <div className="col-4">
        
        </div>
      </div>
    </div>
  );
}

export default App;
