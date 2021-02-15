function CreateAlert() {
    alert("Welcom to js from Blazor")
}
function CreatePrompte(question) {

    return prompt(question)
}
function setElementTextById(id, text) {
    document.getElementById(id).innerText = text;

}
function SetElementFocus(element) {
    element.focus()
}
function randomize(maxNumber) {
    DotNet.invokeMethodAsync('EmployeeManagement.Web', 'Randomize', maxNumber)
        .then(result => {
            setElementTextById("randomNumber", result)
        })
        ;
}


window.downloadReport = function (p_strServerFilePath) {
    var link = document.createElement('a');
    link.target = "_blank";
    link.rel = "noopener noreferrer";
    link.href = 'https://localhost:44352/api/employees/GenerateReport';// + this.encodeURIComponent(p_strServerFilePath);
    document.body.appendChild(link);
    link.click();
    document.body.removeChild(link);
}