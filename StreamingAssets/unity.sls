#!r6rs
;; Library for common unity helper functions. Very little here for now but will
;; be expanded.
(library (unity)
         (export debug-log)
         (import (rnrs)
                 (ironscheme clr))
         (clr-using UnityEngine)

(define (debug-log s)
  (clr-static-call Debug Log s))
)
