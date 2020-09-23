import React from 'react'

const ButtonLike = (props) => {
    function funcionson(){
        props.dandoclic(props.dandoitem)
    }
    return (
        <button className="btn btn-info btn-sm float-right" onClick={funcionson}>{props.n} Like(s)</button>
    )
}
export default ButtonLike
