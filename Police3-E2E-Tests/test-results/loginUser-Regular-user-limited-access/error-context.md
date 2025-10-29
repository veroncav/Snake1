# Page snapshot

```yaml
- generic [ref=e1]:
  - button "← Avaleht" [ref=e2] [cursor=pointer]
  - banner [ref=e3]:
    - heading "Politsei Infosüsteem" [level=1] [ref=e5]
    - img "Politsei logo" [ref=e7]
  - heading "Logi sisse" [level=2] [ref=e8]
  - generic [ref=e9]:
    - text: "Kasutaja:"
    - textbox [ref=e10]: kasutaja
    - text: "Parool:"
    - textbox [active] [ref=e11]: "12345"
    - button "Logi sisse" [ref=e12] [cursor=pointer]
    - button "Registreeru" [ref=e13] [cursor=pointer]
    - button "GitHub" [ref=e14] [cursor=pointer]
  - img "Photo" [ref=e16]
```